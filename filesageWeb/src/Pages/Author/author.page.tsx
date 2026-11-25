import { useEffect, useState } from "react";
import { AuthorDatastore } from "../../Data/Api/author.datastore";
import type { AuthorEntity } from "../../Data/Types/Entities/author.entity";
import { Table } from "../../UI/Components/Table/Table";
import { AUTHOR_COLUMNS } from "./author.columns";
import { useModal } from "../../Data/Context/modal.context";
import { AuthorForm } from "./author.form";

export const AuthorPage = () => {
    const { setModal, isOpen } = useModal();

    const authorDatastore = new AuthorDatastore();

    const [tableData, setTableData] = useState<AuthorEntity[]>([]);
    const [authorRequestLoading, setAuthorLoading] = useState(false)

    useEffect(() => {
        setAuthorLoading(true)
        const loadAuthors = async () => {
            const request = await authorDatastore.getAuthor();
            if (!request.error) { setTableData(authorDatastore.castRequestAuthorToEntity(request.data)); }
        }
        
        void loadAuthors();
        setAuthorLoading(false)
    }, [isOpen])


    return (
        <>
            <button onClick={() => setModal(<AuthorForm formOperation="ins" />)}>Novo Author</button>
            <Table
                data={tableData}
                tableColumns={AUTHOR_COLUMNS}
                loading={authorRequestLoading}
                keyExtractor={(value) => value.archiveId}
            />
        </>
    )
}