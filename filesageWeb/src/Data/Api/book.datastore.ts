import type { BookEntity, BookRequest } from "../Types/Entities/book.entity";
import { Datastore } from "./datastore";

export class BookDatastore extends Datastore {

    private urlSufix = 'book'

    public async getBook() {
        return this.getRequest(this.urlSufix);
    }

    public async getBookId(id: number) {
        return this.getRequest(this.urlSufix + `/${id}`);
    }

    public async postBook(body: BookEntity) {
        return this.postRequest(this.urlSufix, body);
    }
    public async patchBook(body: BookEntity) {
        return this.postRequest(this.urlSufix, body, 'PATCH');
    }

    public castRequestBookToEntity(responseBody: BookRequest[]): BookEntity[] {

        return responseBody.map((book) => ({
            archiveId: book.id,
            registeredAt: book.registeredAt,
            updatedAt: book.updatedAt,
            isActive: book.isActive,
            tags: book.tags,
            urls: book.urls,
            ...book.book,
        } as BookEntity));
    }
}