import { createContext, createElement, useContext, useState, type ReactNode } from "react";
import style from './notification.module.css'
import { ProjectConfig } from "../../Config/ProjectConfig";

export type NotificationType = "success" | "failure" | "warning";
export type ShowNotificationType = (message: string, type: NotificationType) => void;

type Notification = {
    message: string;
    type: NotificationType;
}

interface NotificationContextProps {
    showNotification: (message: string, type: NotificationType) => void,
}

const NotificationContext = createContext<NotificationContextProps | undefined>(undefined);

export function NotificationProvider({ children }: { children: ReactNode }) {
    const [notification, setNotification] = useState<Notification | null>(null)

    function showNotification(message: string, type: NotificationType, time = ProjectConfig.notificationTime) {
        setNotification({ message, type })

        setTimeout(() => {
            setNotification(null)
        }, time)
    }
    return createElement(
        NotificationContext.Provider,
        {
            value: {
                showNotification,
            },
        },
        children,
        notification && createElement(
            'div',
            { className: `${style.notification} ${style[notification.type]}` },
            createElement('h3', null, notification.type),
            createElement('span', null, notification.message)
        )
    );
}

export const useNotification = () => {
    const context = useContext(NotificationContext);

    if (!context) throw new Error("useNotification must be used within a NotificationProvider");

    return context;
};