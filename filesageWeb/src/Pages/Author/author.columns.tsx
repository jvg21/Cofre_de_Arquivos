import type { AuthorEntity } from "../../Data/Types/Entities/author.entity";
import type { TableColumn } from "../../UI/Components/Table/Table";

export const AUTHOR_COLUMNS: TableColumn<AuthorEntity>[] = [
    { key: "archiveId", label: "Id" },
    { key: "name", label: "Name" },
] 