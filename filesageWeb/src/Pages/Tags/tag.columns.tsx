import type { TableColumn } from "../../UI/Components/Table/Table";
import type { TagEntity } from "../../Data/Types/Entities/tag.entity";

export const TAG_COLUMNS: TableColumn<TagEntity>[] = [
    { key: "id", label: "Id" },
    { key: "type", label: "Type" },
    { key: "content", label: "Content" },
    { key: "isActive", label: "IsActive" },
]
