import { useState, type FormEvent } from "react";
import type { FormOperation } from "../../Data/Types/common.type";
import formStyle from "../../UI/Styles/form.module.css";
import type { AuthorEntity } from "../../Data/Types/Entities/author.entity";
import { useNotification } from "../../Data/Context/notification.context";
import { AuthorDatastore } from "../../Data/Api/author.datastore";
import { useModal } from "../../Data/Context/modal.context";
import type { UrlEntity } from "../../Data/Types/Entities/url.entity";

interface AuthorFormProps {
    formOperation: FormOperation
}

export const AuthorForm = ({ formOperation }: AuthorFormProps) => {
    const { showNotification } = useNotification();
    const { closeModal } = useModal();
    const authorDatastore = new AuthorDatastore();

    const [formData, setData] = useState<Partial<AuthorEntity>>(reset())
    const [urlInput, setInput] = useState<Partial<UrlEntity>>({})

    function reset(): Partial<AuthorEntity> {
        return ({
            name: "",
            urls: [],
        })
    }

    async function handleInsert() {
        const request = await authorDatastore.postAuthor([formData]);
        showNotification(request.message, request.error ? 'failure' : 'success')
        return;
    }

    async function handleUpdate() {

    }

    async function handleDelete() {

    }

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

                <label htmlFor="name">Name: </label>
                <input id="name" type="text" placeholder="name" value={formData.name || ""}
                    onChange={(e) => setData((prev) => ({ ...prev, name: e.target.value }))}

                />

                <label>Urls: </label>


                <button type="submit">Enviar</button>
            </form>
        </div>
    )
};