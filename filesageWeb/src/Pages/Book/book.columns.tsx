import type { BookEntity } from "../../Data/Types/Entities/book.entity";
import type { TableColumn } from "../../UI/Components/Table/Table";

export const BOOK_COLUMNS: TableColumn<BookEntity>[] = [
    { key: "archiveId", label: "Id" },
    { key: "title", label: "Title" },
    { key: "summary", label: "Summary" },
    { key: "rating", label: "Rating" },
    { key: "words", label: "Words" },
    {
        key: "currentChapters", label: "Current/Total Chapters",
        render: (value, row) => `${value ?? "-"} / ${row.totalChapters !== 0 ? row.totalChapters : "-"}`
    }
    // { key: "totalChapters", label: "Total Chapters" },

] 