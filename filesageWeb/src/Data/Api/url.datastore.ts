import type { UrlEntity } from "../Types/Entities/url.entity";
import { Datastore } from "./datastore";

export class UrlDatastore extends Datastore {

    private urlSufix = 'url'

    public async getUrl() {
        return this.getRequest(this.urlSufix);
    }

    public async getUrlId(id: number) {
        return this.getRequest(this.urlSufix + `/${id}`);
    }

    public async postUrl(body: UrlEntity) {
        return this.postRequest(this.urlSufix, body);
    }
    public async postUrlName(body: { name: string }) {
        return this.postRequest(this.urlSufix+"/name", body);
    }
    public async patchUrl(body: UrlEntity) {
        return this.postRequest(this.urlSufix, body, 'PATCH');
    }
}