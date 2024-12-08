export const paths = {
  home: {
    path: '/',
    getHref: () => '/',
  },
  employee: {
    root: {
      path: '/employee',
      getHref: () => '/employee',
    },
    dashboard: {
      path: '',
      getHref: () => '/employee',
    },
    orders: {
      path: 'orders',
      getHref: () => '/employee/orders',
    },
    updateOrder: {
      path: 'orders/:orderId',
      getHref: (orderId: number) => `/employee/orders/${orderId}`,
    },
    newOrder: {
      path: 'orders/new',
      getHref: () => '/employee/orders/new',
    },
  },
  management: {
    root: {
      path: '/management',
      getHref: () => '/management',
    },
    products: {
      path: 'products',
      getHref: () => '/management/products',
    },
    updateProduct: {
      path: 'products/:productId',
      getHref: (productId: number) => `/management/products/${productId}`,
    },
    addProduct: {
      path: 'products/add',
      getHref: () => '/management/products/add',
    },
    taxes: {
      path: 'taxes',
      getHref: () => '/management/taxes',
    },
    updateTax: {
      path: 'taxes/:taxId',
      getHref: (taxId: number) => `/management/taxes/${taxId}`,
    },
    addTax: {
      path: 'taxes/add',
      getHref: () => '/management/taxes/add',
    },
    modifiers: {
      path: 'modifiers',
      getHref: () => '/management/modifiers',
    },
    updateModifier: {
      path: 'modifiers/:modifierId',
      getHref: (modifierId: number) => `/management/modifiers/${modifierId}`,
    },
    addModifier: {
      path: 'modifiers/add',
      getHref: () => '/management/modifiers/add',
    },
    serviceCharges: {
      path: 'service-charges',
      getHref: () => '/management/service-charges',
    },
    updateServiceCharge: {
      path: 'service-charges/:serviceChargeId',
      getHref: (serviceChargeId: number) => `/management/service-charges/${serviceChargeId}`,
    },
    addServiceCharge: {
      path: 'service-charges/add',
      getHref: () => '/management/service-charges/add',
    },
    discounts: {
      path: 'discounts',
      getHref: () => '/management/discounts',
    },
    updateDiscount: {
      path: 'discounts/:discountId',
      getHref: (discountId: number) => `/management/discounts/${discountId}`,
    },
    addDiscount: {
      path: 'discounts/add',
      getHref: () => '/management/discounts/add',
    },
    giftCards: {
      path: 'gift-cards',
      getHref: () => '/management/gift-cards',
    },
    updateGiftCard: {
      path: 'gift-cards/:giftCardId',
      getHref: (giftCardId: number) => `/management/gift-cards/${giftCardId}`,
    },
    addGiftCard: {
      path: 'gift-cards/add',
      getHref: () => '/management/gift-cards/add',
    },
  },
} as const;
