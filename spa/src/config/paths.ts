export const paths = {
  home: {
    path: '/',
    getHref: () => '/',
  },
  login: {
    path: '/login',
    getHref: () => '/login',
  },
  businessManagement: {
    root: {
      path: '/business-management',
      getHref: () => '/business-management',
    },
    employees: {
      path: 'employees',
      getHref: () => '/business-management/employees',
    },
    updateEmployee: {
      path: 'employees/:employeeId',
      getHref: (employeeId: number) => `/business-management/employees/${employeeId}`,
    },
    newEmployee: {
      path: 'employees/new',
      getHref: () => '/business-management/employees/new',
    },
    businesses: {
      path: 'businesses',
      getHref: () => '/business-management/businesses',
    },
    newBusiness: {
      path: 'businesses/new',
      getHref: () => '/business-management/businesses/new',
    },
    updateBusiness: {
      path: 'businesses/:businessId',
      getHref: (businessId: number) => `/business-management/businesses/${businessId}`,
    },
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
    reservations: {
      path: 'reservations',
      getHref: () => '/employee/reservations',
    },
    updateReservation: {
      path: 'reservations/:reservationId',
      getHref: (reservationId: number) => `/employee/reservations/${reservationId}`,
    },
    newReservation: {
      path: 'reservations/new',
      getHref: () => '/employee/reservations/new',
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
    services: {
      path: 'services',
      getHref: () => '/management/services',
    },
    updateService: {
      path: 'services/:serviceId',
      getHref: (serviceId: number) => `/management/services/${serviceId}`,
    },
    addService: {
      path: 'services/add',
      getHref: () => '/management/services/add',
    },
  },
} as const;
