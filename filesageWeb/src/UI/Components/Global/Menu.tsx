import { useEffect, useState } from "react"
import styles from './global.components.module.css'
import { MODULES } from "../../../Config/Modules";
import { Link } from "react-router-dom";

export const Menu = () => {
    const [isOpen, setOpen] = useState<boolean>(false);

    function handleOpenMenu(e: KeyboardEvent) {
        if (e.code === 'KeyE') {
            setOpen((prev) => !prev)
        }
    }
    useEffect(() => {

        window.addEventListener('keypress', handleOpenMenu)

        return () => {
            window.removeEventListener('keypress', handleOpenMenu)
        }
    }, [])

    return (
        <div className={`${styles.menuBox}  ${isOpen ? styles.menuOpen : ""}`}>
            <div className={`${styles.menuWheel}`}>


                <ul>
                    {
                        MODULES.map((module) => (
                            <li key={module.name}>
                                <Link to={module.url}>{<module.icon />} {module.name} </Link>
                            </li>
                        ))
                    }
                </ul>
            </div>
        </div>
    )
}