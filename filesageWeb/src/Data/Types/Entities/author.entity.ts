import type { TagEntity } from "./tag.entity"
import type { UrlEntity } from "./url.entity"

export type AuthorRequest = {
    id: number,
    author: {
        name: string,
        // books: []
    },
    urls: UrlEntity[],
    tags: TagEntity[],
    registeredAt: string,
    updatedAt: string,
    isActive: boolean
}

export type AuthorEntity = {
    archiveId: number,
    name: string,

    urls: UrlEntity[],
    tags: TagEntity[],

    registeredAt: string,
    updatedAt: string,
    isActive: boolean
}