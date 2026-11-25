import type { AuthorEntity, AuthorRequest } from "../Types/Entities/author.entity";
import { Datastore } from "./datastore";

export class AuthorDatastore extends Datastore {

    private urlSufix = 'author'

    public async getAuthor() {
        return this.getRequest(this.urlSufix);
    }

    public async getAuthorId(id: number) {
        return this.getRequest(this.urlSufix + `/${id}`);
    }

    public async postAuthor(body: Partial<AuthorEntity>[]) {
        return this.postRequest(this.urlSufix, body);
    }
    public async patchAuthor(body: Partial<AuthorEntity>) {
        return this.postRequest(this.urlSufix, body, 'PATCH');
    }

    public castRequestAuthorToEntity(responseBody: AuthorRequest[]): AuthorEntity[] {

        return responseBody.map((author) => ({
            archiveId: author.id,
            registeredAt: author.registeredAt,
            tags: author.tags,
            urls: author.urls,
            updatedAt: author.updatedAt,
            isActive: author.isActive,
            ...author.author,
        } as AuthorEntity));
    }

}