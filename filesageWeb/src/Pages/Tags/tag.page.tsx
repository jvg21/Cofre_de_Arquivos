import { useEffect, useState } from "react"
import { Table } from "../../UI/Components/Table/Table"
import { TAG_COLUMNS } from "./tag.columns"
import type { TagEntity } from "../../Data/Types/Entities/tag.entity"
import { TagDatastore } from "../../Data/Api/tag.datastore"

export const TagPage = () => {
    const tagDatastore = new TagDatastore();
    const [tableData, setTableData] = useState<TagEntity[]>([]);
    const [tagRequestLoading, setTagLoading] = useState(false)

    useEffect(() => {
        const loadTags = async () => {
            const request = await tagDatastore.getTag();
            if (!request.error) setTableData(request.data);
        };

        void loadTags();
    }, [])
    return (
        <div>
            <Table
                data={tableData}
                loading={tagRequestLoading}
                tableColumns={TAG_COLUMNS}
                keyExtractor={(value) => value.id}
            />
        </div>
    )
}