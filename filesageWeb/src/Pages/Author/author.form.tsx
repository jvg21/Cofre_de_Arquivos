import { useState, type FormEvent } from "react";
import type { FormOperation } from "../../Data/Types/common.types";
import formStyle from "../../UI/Styles/form.module.css";
import type { AuthorEntity, AuthorPatchRequest, AuthorPostRequest } from "../../Data/Types/Entities/author.entity";
import { useNotification } from "../../Data/Context/notification.context";
import { AuthorDatastore } from "../../Data/Api/author.datastore";
import { useModal } from "../../Data/Context/modal.context";
import type { UrlEntity } from "../../Data/Types/Entities/url.entity";

interface AuthorFormProps {
    formOperation: FormOperation,
    initialFormData?: Partial<AuthorEntity>
}

export const AuthorForm = ({ formOperation, initialFormData }: AuthorFormProps) => {
    const { closeModal } = useModal();
    const { showNotification } = useNotification();
    const authorDatastore = new AuthorDatastore();

    const [formData, setData] = useState<Partial<AuthorEntity>>(initialFormData ?? reset())
    const [urls, setUrls] = useState<UrlEntity[]>(formData.urls || [])
    const [urlInput, setInput] = useState<Partial<UrlEntity>>({})


    function reset(): Partial<AuthorEntity> {
        return ({ name: undefined, urls: [], tags: [] })
    }

    function handleAddUrl() {
        const content = urlInput.content?.trim();
        if (!content) return;

        setUrls((prev) => [...prev, { ...urlInput, content } as UrlEntity]);
        setInput({});
    }

    function handleRemoveUrl(removeIndex: number,) {
        setUrls((prev) => prev.filter((_, urlIndex) => urlIndex !== removeIndex));
    }

    async function handleInsert() {
        if (!formData.name) return;

        const requestBody: AuthorPostRequest = {
            name: formData.name,
            urls: urls,
            tagsLink: []
        }
        const request = await authorDatastore.postAuthor([requestBody]);
        showNotification(request.message, request.error ? 'failure' : 'success')
        return;
    }

    async function handleUpdate() {
        if (!formData.name || !formData.archiveId) return;
        const effectiveUrls = urls;

        const urlsInsert = effectiveUrls.filter((url) => !url.id).map(({ name, content }) => ({ name, content }));

        const urlsRemove = (initialFormData?.urls ?? [])
            .filter((initialUrl) => initialUrl.id !== undefined && !effectiveUrls.some((url) => url.id === initialUrl.id))
            .map((url) => url.id as number);

        const requestBody: AuthorPatchRequest = {
            id: formData.archiveId,
            name: formData.name,
            urlsInsert,
            urlsRemove,
            tagsLink: [],
            tagsUnlink: [],
            isActive: formData.isActive
        }

        const request = await authorDatastore.patchAuthor([requestBody]);
        showNotification(request.message, request.error ? 'failure' : 'success')
        return;
    }

    async function handleDelete() { }

    async function handleSubmit(e: FormEvent<HTMLFormElement>) {
        e.preventDefault();
        switch (formOperation) {
            case "ins":
                await handleInsert()
                break;

            case "upd":
                await handleUpdate()
                break;

            case "del":
                await handleDelete()
                break;

        }
        setData(reset());

        closeModal();
    }

    return (
        <div className={formStyle.wrapper}>
            <form className={formStyle.form} onSubmit={handleSubmit}>

                <label htmlFor="archiveId">Id: </label>
                <input id="archiveId" type="text" value={formData.archiveId || ""} disabled />

                <label htmlFor="name">Name: </label>
                <input id="name" type="text" placeholder="name" value={formData.name || ""}
                    onChange={(e) => setData((prev) => ({ ...prev, name: e.target.value }))}
                />

                <label>Urls: </label>
                <div className={formStyle.inputRow}>
                    <input id="urlname" type="text" placeholder="name" value={urlInput.name || ""}
                        onChange={(e) => setInput((prev) => ({ ...prev, name: e.target.value }))}
                    />

                    <input id="urlcontent" type="text" placeholder="content" value={urlInput.content || ""}
                        onChange={(e) => setInput((prev) => ({ ...prev, content: e.target.value }))}
                    />
                    <button type="button" onClick={() => { handleAddUrl(); setInput(reset()) }}>+</button>
                </div>

                <div>
                    <ul>
                        {
                            urls.map((url, index) => (
                                <li key={index}>{url.id}-{url.name}-{url.content}
                                    <button type="button" onClick={() => handleRemoveUrl(index)}>
                                        x
                                    </button>
                                </li>
                            ))
                        }
                    </ul>
                </div>


                <button type="submit">Enviar</button>
            </form>
        </div>
    )
};