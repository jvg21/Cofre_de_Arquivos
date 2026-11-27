import type { AuthorEntity } from "../../Data/Types/Entities/author.entity";
import type { UrlEntity } from "../../Data/Types/Entities/url.entity";
import type { TableColumn } from "../../UI/Components/Table/Table";

export const AUTHOR_COLUMNS: TableColumn<AuthorEntity>[] = [
    { key: "archiveId", label: "Id" },
    { key: "name", label: "Name" },
    {
        key: "urls", label: "Urls",
        render: (value) => {
            if (!Array.isArray(value)) return "-"
            return (
                <ul>{
                    value.map((ulr: UrlEntity) =>
                        <li>
                            <a href={ulr.content} target="_blank">
                                {ulr.name ?? ulr.content.slice(0, 15)}
                            </a>
                        </li>)
                }
                </ul>
            )
        }
    }
] 