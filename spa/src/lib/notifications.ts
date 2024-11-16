import { DefaultMantineColor } from '@mantine/core';
import { notifications } from '@mantine/notifications';

type NotificationType = 'success' | 'failure';

type NotificationProps = {
  type: NotificationType;
  title: string;
  message?: string;
};

export const showNotification = (props: NotificationProps) => {
  notifications.show({
    title: props.title,
    message: props.message,
    color: getNotificationColor(props.type),
    withBorder: true,
  });
};

const getNotificationColor = (type: NotificationType): DefaultMantineColor => {
  switch (type) {
    case 'success':
      return 'teal';
    case 'failure':
      return 'red';
    default:
      throw Error(`Unknown notification type: ${type}`);
  }
};
