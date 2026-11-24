export type BookEntity = {
    archiveId:number,
    title: string,
    summary: string,
    notes?: string,
    rating?:number,
    words?:number,
    totalChapters?:number,
    currentChapters?:number,
    readingStatus?:number,
    writingStatus?:number,
    published:string,
    lastUpdate:string

}