import { Center, Pagination, SimpleGrid, Stack } from '@mantine/core';
import { useProducts } from '../../product/api/get-products';
import { useMemo, useState } from 'react';
import { EnhancedCreateOrderItemInput, OrderProduct } from './order-product';
import { CreateOrUpdateOrderItemInput, useCreateOrder } from '../api/create-order';
import { showNotification } from '@/lib/notifications';
import { useNavigate } from 'react-router-dom';
import { paths } from '@/config/paths';
import { OrderItemList } from './order-item-list';
import { useUpdateOrder } from '../api/update-order';
import { useServiceCharges } from '@/features/service-charge/api/get-service-charges';

type OrderProductsProps = {
  orderId?: number;
  orderItems?: EnhancedCreateOrderItemInput[];
  selectedServiceCharges?: string[];
};

export const OrderProducts = (props: OrderProductsProps) => {
  const [page, setPage] = useState<number>(1);
  const productsQuery = useProducts({ paginationFilter: { page, itemsPerPage: 50 } });
  const [orderItems, setOrderItems] = useState<EnhancedCreateOrderItemInput[]>(props.orderItems ?? []);
  const serviceChargesQuery = useServiceCharges({ paginationFilter: { page: 1, itemsPerPage: 50 } });
  const navigate = useNavigate();

  const createOrderMutation = useCreateOrder({
    mutationConfig: {
      onSuccess: () => {
        showNotification({
          type: 'success',
          title: 'Order created successfully.',
        });

        navigate(paths.employee.orders.getHref());
      },
    },
  });

  const updateOrderMutation = useUpdateOrder({
    mutationConfig: {
      onSuccess: () => {
        showNotification({
          type: 'success',
          title: 'Order modified successfully.',
        });
      },
    },
  });

  const totalPages = useMemo(() => {
    if (productsQuery.data == null) {
      return 0;
    }

    return Math.ceil(productsQuery.data.totalItems / productsQuery.data.itemsPerPage);
  }, [productsQuery.data]);

  const addOrderItem = (orderItem: EnhancedCreateOrderItemInput) => {
    setOrderItems((prev) => [...prev, orderItem]);
  };

  const updateOrderItem = (orderItem: EnhancedCreateOrderItemInput) => {
    console.log(orderItem);
    setOrderItems((prev) => prev.map((x) => (x.cartItemId === orderItem.cartItemId ? orderItem : x)));
  };

  const removeOrderItem = (orderItem: EnhancedCreateOrderItemInput) => {
    setOrderItems((prev) => prev.filter((x) => x.cartItemId !== orderItem.cartItemId));
  };

  const createOrUpdateOrder = (serviceChargeIds: number[]) => {
    const mappedItems = orderItems.map(
      (x): CreateOrUpdateOrderItemInput => ({
        productId: x.productId,
        modifierIds: x.modifierIds,
        quantity: x.quantity,
      }),
    );

    if (props.orderId) {
      updateOrderMutation.mutate({ orderId: props.orderId, data: { orderItems: mappedItems, serviceChargeIds } });
    } else {
      createOrderMutation.mutate({ data: { orderItems: mappedItems, serviceChargeIds } });
    }
  };

  if (productsQuery.isLoading || serviceChargesQuery.isLoading) {
    return <div>loading..</div>;
  }

  const products = productsQuery.data?.items;
  const serviceCharges = serviceChargesQuery.data?.items;
  if (!products || !serviceCharges) {
    return null;
  }

  return (
    <Stack>
      <OrderItemList
        orderItems={orderItems}
        updateOrderItem={updateOrderItem}
        removeOrderItem={removeOrderItem}
        onConfirm={createOrUpdateOrder}
        confirmText={props.orderId ? 'Save' : 'Create order'}
        isLoading={props.orderId ? updateOrderMutation.isPending : createOrderMutation.isPending}
        serviceCharges={serviceCharges}
        selectedServiceCharges={props.selectedServiceCharges ?? []}
      />

      <SimpleGrid cols={4}>
        {products.map((product) => (
          <OrderProduct product={product} addToOrder={addOrderItem} key={product.id} orderItems={orderItems} />
        ))}
      </SimpleGrid>

      <Center>
        <Pagination total={totalPages} value={page} onChange={setPage} />
      </Center>
    </Stack>
  );
};
