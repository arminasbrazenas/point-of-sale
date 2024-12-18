export enum OrderStatus {
  Open = 'Open',
  Completed = 'Completed',
  Closed = 'Closed',
  Canceled = 'Canceled',
  RefundInitiated = 'RefundInitiated',
  Refunded = 'Refunded',
}

export enum PricingStrategy {
  FixedAmount = 'FixedAmount',
  Percentage = 'Percentage',
}

export enum PaymentMethod {
  Cash = 'Cash',
  GiftCard = 'GiftCard',
  Card = 'Card',
}

export enum PaymentStatus {
  Pending = 'Pending',
  Canceled = 'Canceled',
  Succeeded = 'Succeeded',
}

export enum DiscountType {
  Predefined = 'Predefined',
  Flexible = 'Flexible',
}

export enum DiscountTarget {
  Product = 'Product',
  Order = 'Order',
}

export enum ReservationStatus {
  Active = 'Active',
  Canceled = 'Canceled',
  Completed = 'Completed',
  InProgress = 'InProgress',
}

export type EntityBase = {
  id: number;
  createdAt: string;
  modifiedAt: string;
};

export type Entity<T> = {
  [K in keyof T]: T[K];
} & EntityBase;

export type PaginationFilter = {
  page: number;
  itemsPerPage: number;
};

export type PagedResponse<T> = {
  page: number;
  itemsPerPage: number;
  totalItems: number;
  items: T[];
};

export type ErrorResponse = {
  errorMessage: string;
};

export type Product = Entity<{
  name: string;
  basePrice: number;
  priceDiscountExcluded: number;
  price: number;
  stock: number;
  taxes: Tax[];
  modifiers: Modifier[];
  discounts: Discount[];
}>;

export type Tax = Entity<{ name: string; rate: number }>;

export type OrderItemModifier = Entity<{ modifierId?: number; name: string; price: number }>;

export type OrderDiscount = Entity<{
  id: number;
  amount: number;
  pricingStrategy: PricingStrategy;
  appliedAmount: number;
  type: DiscountType;
}>;

export type OrderItem = Entity<{
  productId?: number;
  name: string;
  quantity: number;
  unitPrice: number;
  totalPrice: number;
  modifiers: OrderItemModifier[];
  discounts: OrderDiscount[];
  discountsTotal: number;
  taxTotal: number;
}>;

export type OrderServiceCharge = Entity<{
  name: string;
  amount: number;
  pricingStrategy: PricingStrategy;
  appliedAmount: number;
}>;

export type Order = Entity<{
  orderItems: OrderItem[];
  totalPrice: number;
  status: OrderStatus;
  serviceCharges: OrderServiceCharge[];
  discounts: OrderDiscount[];
  reservation?: Reservation;
}>;

export type OrderReceipt = {
  totalPrice: number;
  orderItems: OrderItem[];
  serviceCharges: OrderServiceCharge[];
  discounts: OrderDiscount[];
  reservation?: Reservation;
};

export type Modifier = Entity<{ name: string; priceTaxExcluded: number; price: number; stock: number }>;

export type ServiceCharge = Entity<{ name: string; pricingStrategy: PricingStrategy; amount: number }>;

export type Discount = Entity<{
  amount: number;
  pricingStrategy: PricingStrategy;
  validUntil: string;
  appliesToProductIds?: number[];
  target: DiscountTarget;
}>;

export type Payment = Entity<{
  amount: number;
  method: PaymentMethod;
  status: PaymentStatus;
}>;

export type CashPayment = Payment;

export type GiftCardPayment = Payment & {
  giftCardCode: string;
};

export type OrderPayments = {
  payments: Payment[];
  paidAmount: number;
  unpaidAmount: number;
  totalAmount: number;
};

export type GiftCard = Entity<{
  code: string;
  amount: number;
  expiresAt: string;
  usedAt?: string;
}>;

export type Tip = Entity<{
  amount: number;
}>;

export type PaymentIntent = {
  paymentIntentId: string;
  clientSecret: string;
};

export type ApplicationUser = {
  id: number;
  businessId: number | null;
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber: string;
  role: string;
};

export const Roles = {
  Admin: 'Admin',
  BusinessOwner: 'BusinessOwner',
  Employee: 'Employee',
} as const;

export type Business = {
  id: number;
  name: string;
  address: string;
  email: string;
  phoneNumber: string;
};

export type ServiceEmployee = {
  id: number;
  fullName: string;
};

export type Service = Entity<{
  name: string;
  price: number;
  durationInMinutes: number;
  providedByEmployees: ServiceEmployee[];
}>;

export type ReservationCustomer = {
  firstName: string;
  lastName: string;
};

export type ReservationDate = {
  start: string;
  end: string;
};

export type Reservation = Entity<{
  description: string;
  customer: ReservationCustomer;
  date: ReservationDate;
  status: ReservationStatus;
  employee: ServiceEmployee;
  serviceId: number;
  price: number;
}>;
