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
  },
} as const;
