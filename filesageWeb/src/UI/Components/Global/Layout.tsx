import { Outlet } from "react-router-dom"
import layoutStyle from './global.components.module.css'
import { Menu } from "./Menu"

export const Layout = () => {
    return (
        <>
            <Menu />
            <div className={layoutStyle.header}>
                a
            </div>
            <div className={layoutStyle.outlet}>
                <Outlet />
            </div>
        </>
    )
}