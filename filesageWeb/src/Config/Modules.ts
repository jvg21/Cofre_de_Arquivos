import type { IconType } from "react-icons"
import { FaBook, FaTags } from "react-icons/fa"
import { FaPerson } from "react-icons/fa6"

type Module = {
    name: string,
    description: string,
    url: string,
    icon: IconType,
    hidden?: boolean
}

export const MODULES: Module[] = [
    {
        name: "Books",
        description: "Books module",
        url: "/books",
        icon: FaBook
    },
    {
        name: "Authors",
        description: "Authors module",
        url: "/authors",
        icon: FaPerson
    },
    {
        name: "Tags",
        description: "Tag module",
        url: "/tags",
        icon: FaTags
    },
    {
        name: "Comics",
        description: "Comics module",
        url: "/comics",
        icon: FaTags,
        hidden:true,
    },
    {
        name: "Notes",
        description: "Notes module",
        url: "/notes",
        icon: FaTags,
        hidden:true
    },
    {
        name: "Documents",
        description: "Documents module",
        url: "/documents",
        icon: FaTags,
        hidden:true,
    }
]