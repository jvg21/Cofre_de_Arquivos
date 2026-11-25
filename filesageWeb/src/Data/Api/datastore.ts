import type { RequestReturn } from "../Types/RequestReturn";

export class Datastore {

    private urlBase = "https://localhost:7016/";

    public async getRequest(url: string): Promise<RequestReturn> {
        const response = { error: false } as RequestReturn;

        try {
            const request = await fetch(this.urlBase + url, {
                method: 'GET'
            });
            const requestJson = await request.json();
            response.status = request.status;

            if (request.status !== 200) throw new Error(requestJson.message);

            response.message = "success"
            response.data = requestJson;
        } catch (e) {
            response.error = true;
            if (e instanceof Error) response.message = e.message;
        }
        return response;
    }

    public async postRequest(url: string, body: any = {}, method: string = "POST"): Promise<RequestReturn> {
        const response = { error: false } as RequestReturn;
        try {
            const request = await fetch(this.urlBase + url, {
                method: method,
                headers: { "Content-Type": "application/json" },
                body: body === undefined ? undefined : JSON.stringify(body),
            });
            const requestJson = await request.json();
            response.status = request.status;

            if (request.status !== 200) throw new Error(requestJson.message);

            response.message = "success"
            response.data = requestJson;
        } catch (e) {
            response.error = true;
            if (e instanceof Error) response.message = e.message;
        }
        return response;
    }
}