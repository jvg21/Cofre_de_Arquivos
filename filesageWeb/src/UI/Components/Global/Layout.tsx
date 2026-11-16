import { Outlet } from "react-router-dom"
import layoutStyle from '../../Styles/layout.module.css'

export const Layout = () => {
    return (
        <>
            <div className={layoutStyle.header}> 
a
            </div>
            <div className={layoutStyle.outlet}>
                <Outlet />
            </div>
        </>
    )
}