import { useState, type FormEvent } from "react";
import type { FormOperation } from "../../Data/Types/common.types";
import formStyle from "../../UI/Styles/form.module.css";
import { useNotification } from "../../Data/Context/notification.context";
import { BookDatastore } from "../../Data/Api/book.datastore";
import { useModal } from "../../Data/Context/modal.context";
import type { UrlEntity } from "../../Data/Types/Entities/url.entity";
import type { BookEntity, BookPatchRequest, BookPostRequest } from "../../Data/Types/Entities/book.entity";

interface BookFormProps {
    formOperation: FormOperation,
    initialFormData?: Partial<BookEntity>
}

export const BookForm = ({ formOperation, initialFormData }: BookFormProps) => {
    const { showNotification } = useNotification();
    const { closeModal } = useModal();
    const bookDatastore = new BookDatastore();

    const [formData, setData] = useState<Partial<BookEntity>>(initialFormData ?? reset())
    const [urls, setUrls] = useState<UrlEntity[]>(formData.urls || [])
    const [urlInput, setInput] = useState<Partial<UrlEntity>>({})


    function reset(): Partial<BookEntity> {
        return ({ title: "", urls: [], tags: [] })
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
        if (!formData.title) return;

        const urlInput = urls
        const requestBody: BookPostRequest = {
            title: formData.title,
            summary: formData.summary,
            notes: formData.notes,
            lastUpdate: formData.lastUpdate,
            readingStatus: formData.readingStatus || 0,
            writingStatus: formData.writingStatus,
            words: formData.words,
            currentChapter: formData.currentChapters,
            published: formData.published,
            rating: formData.rating,
            totalChapters: formData.totalChapters,
            urls: urlInput,
            tagsLink: []
        }
        const request = await bookDatastore.postBook([requestBody]);
        showNotification(request.message, request.error ? 'failure' : 'success')
        return;
    }

    async function handleUpdate() {
        if (!formData.title || !formData.archiveId) return;
        const urlInput = urls

        const urlsInsert = urlInput.filter((url) => !url.id);
        const urlsRemove = formData.urls?.
            filter((url) => url.id && initialFormData?.urls?.find((initalUlr) => initalUlr.id === url.id))
            || [];

        const requestBody: BookPatchRequest = {
            id: formData.archiveId,
            title: formData.title,
            urlsInsert,
            urlsRemove: urlsRemove.map((url) => url.id).filter((id): id is number => id !== undefined),
            tagsLink: [],
            tagsUnlink: [],
            isActive: formData.isActive
        }

        const request = await bookDatastore.patchBook([requestBody]);
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

                <label htmlFor="title">Title: </label>
                <input id="title" type="text" placeholder="name" value={formData.title || ""}
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