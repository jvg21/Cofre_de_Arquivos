import type { BookEntity } from "../Types/Entities/book.entity";
import { Datastore } from "./datastore";

export class BookDatastore extends Datastore {

    private urlSufix = 'book'

    public async getBook() {
        return this.getRequest(this.urlSufix);
    }

    public async getTagId(id: number) {
        return this.getRequest(this.urlSufix + `/${id}`);
    }

    public async postBook(body: BookEntity) {
        return this.postRequest(this.urlSufix, body);
    }
    public async patchBook(body: BookEntity) {
        return this.postRequest(this.urlSufix, body, 'PATCH');
    }
}