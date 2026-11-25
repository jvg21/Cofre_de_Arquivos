import type { TagEntity } from "./tag.entity"
import type { UrlEntity } from "./url.entity"

export type BookRequest = {
    id: number,
    book: {
        title: string,
        summary: string,
        notes: string,
        rating: number,
        words: number,
        totalChapters: number,
        currentChapter: number,
        readingStatus: number,
        writingStatus: number,
        authors: [],
        published: string,
        lastUpdate: string
    },
    
    urls: UrlEntity[],
    tags: TagEntity[],
    registeredAt: string,
    updatedAt: string,
    isActive: boolean
}


export type BookEntity = {
    archiveId: number,
    title: string,
    summary: string,
    notes?: string,
    rating?: number,
    words?: number,
    totalChapters?: number,
    currentChapters?: number,
    readingStatus?: number,
    writingStatus?: number,
    published: string,
    lastUpdate: string,

    urls: UrlEntity[],
    tags: TagEntity[],
    registeredAt: string,
    updatedAt: string,
    isActive: boolean

}