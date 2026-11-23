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
        name: "Tag",
        description: "Tag module",
        url: "/tags",
        icon: FaTags
    }
]