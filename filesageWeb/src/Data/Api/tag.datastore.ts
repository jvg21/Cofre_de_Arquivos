import type { TagEntity } from "../../Types/Entities/tag.entity";
import { Datastore } from "./datastore";

export class TagDatastore extends Datastore {

    private urlSufix = 'tag'

    public async getTag() {
        return this.getRequest(this.urlSufix);
    }

    public async getTagId(id: number) {
        return this.getRequest(this.urlSufix + `/${id}`);
    }

    public async postTag(body: TagEntity) {
        return this.postRequest(this.urlSufix, body);
    }
    public async patchTag(body: TagEntity) {
        return this.postRequest(this.urlSufix, body, 'PATCH');
    }
}