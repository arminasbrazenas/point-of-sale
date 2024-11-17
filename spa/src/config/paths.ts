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
  },
} as const;
