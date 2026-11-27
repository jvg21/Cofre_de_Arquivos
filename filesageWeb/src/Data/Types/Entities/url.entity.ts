export interface UrlRequest {
    id?: number,
    name?: string,
    content: string
}

export type UrlEntity = {
    id?: number,
    name?: string,
    content: string,
    archiveId: number
}