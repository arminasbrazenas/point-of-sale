export const paths = {
  home: {
    path: '/',
    getHref: () => '/',
  },
  management: {
    root: {
      path: '/management',
      getHref: () => '/management',
    },
    dashboard: {
      path: '',
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
  },
} as const;
