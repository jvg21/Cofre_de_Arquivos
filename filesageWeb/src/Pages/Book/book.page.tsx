import { useEffect, useState } from "react";
import { BookDatastore } from "../../Data/Api/book.datastore";
import type { BookEntity } from "../../Data/Types/Entities/book.entity";
import { Table } from "../../UI/Components/Table/Table";
import { BOOK_COLUMNS } from "./book.columns";
import { BookForm } from "./book.form";
import { useModal } from "../../Data/Context/modal.context";

export const BookPage = () => {
    const { setModal, isOpen } = useModal();
    const bookDatastore = new BookDatastore();

    const [tableData, setTableData] = useState<BookEntity[]>([]);
    const [bookRequestLoading, setBookLoading] = useState(false)

    useEffect(() => {
        setBookLoading(true);
        const loadBooks = async () => {
            const request = await bookDatastore.getBook();
            if (!request.error) { setTableData(bookDatastore.castRequestBookToEntity(request.data)); }
        }
        void loadBooks();
        setBookLoading(false);
    }, [isOpen])

    return (
        <>
            <button onClick={() => setModal(<BookForm formOperation="ins" />, "md")}>New Book</button>
            <Table
                data={tableData}
                tableColumns={BOOK_COLUMNS}
                loading={bookRequestLoading}
                keyExtractor={(value) => value.archiveId}
            />
        </>
    )
}