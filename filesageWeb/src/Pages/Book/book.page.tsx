import { useEffect, useState } from "react";
import { BookDatastore } from "../../Data/Api/book.datastore";
import type { BookEntity } from "../../Data/Types/Entities/book.entity";
import { Table } from "../../UI/Components/Table/Table";
import {  BOOK_COLUMNS } from "./book.columns";

export const BookPage = () => {
    const bookDatastore = new BookDatastore();
    const [tableData, setTableData] = useState<BookEntity[]>([]);
    const [bookRequestLoading, setBookLoading] = useState(false)

    useEffect(() => {
        const loadBooks = async () => {
            const request = await bookDatastore.getBook();
            if (!request.error) { setTableData(bookDatastore.castRequestBookToEntity(request.data)); }
        }
        void loadBooks();
    }, [])

    return (
        <Table
            data={tableData}
            tableColumns={BOOK_COLUMNS}
            loading={bookRequestLoading}
            keyExtractor={(value) => value.archiveId}
        />
    )
}