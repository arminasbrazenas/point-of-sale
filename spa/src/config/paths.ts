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
      new: {
        path: 'products/new',
        getHref: () => '/management/products/new',
      },
    },
  },
} as const;
