import type { TagEntity } from "./tag.entity"
import type { UrlEntity } from "./url.entity"

export interface AuthorGetRequest {
    id: number,
    author: {
        name: string,
    },
    urls: UrlEntity[],
    tags: TagEntity[],
    registeredAt: string,
    updatedAt: string,
    isActive: boolean
}



export interface AuthorPostRequest {
    name: string,
    urls?: {
        name?: string,
        content: string
    }[],
    tagsLink: number[]
}
export interface AuthorPatchRequest {
    id:number,
    name: string,
    urlsInsert?: {
        name?: string,
        content: string
    }[],
    urlsRemove?: number[]
    tagsLink?: number[]
    tagsUnlink?: number[]
    isActive?: boolean
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

