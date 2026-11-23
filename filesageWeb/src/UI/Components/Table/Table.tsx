import type { Key, ReactNode } from "react"

export type TableColumn<T> = {
    key: keyof T,
    label: string,
    render?: (value: T[keyof T], row: T) => ReactNode
    hidden?: boolean
}

export type TableActions<T> = {
    key: string,
    label: string,
    action: (row: T) => void,
    disabled?: boolean
    hidden?: boolean
}

interface TableOptions {
    pagination?: boolean;
    emptyMessage?: string;
}
interface TableProps<T> {
    data: T[],
    tableColumns: TableColumn<T>[],
    loading: boolean,
    tableActions?: TableActions<T>[],
    keyExtractor?: (row: T) => Key,
    options?: TableOptions
}

export const Table = <T extends Object>({ data, tableColumns, keyExtractor, loading = false, tableActions, options = {} }: TableProps<T>) => {

    const { emptyMessage = "no data" } = options;
    const tableData = [...data];

    const activeTableColumns = tableColumns.filter((tc) => !tc.hidden);

    if (loading) return (<div><p>loading ....</p></div>)

    if (tableData.length === 0) return (<div><p>{emptyMessage}</p></div>)

    return (
        <div >
            <table>
                <thead>
                    <tr>
                        {activeTableColumns.map((column) =>
                        (
                            <th key={String(column.key)}
                                scope="col">{column.label}
                            </th>
                        ))}

                        {tableActions && tableActions.length > 0 && (
                            <th key="actions" scope="col"  >
                                Actions
                            </th>
                        )}
                    </tr>
                </thead>
                <tbody>
                    {
                        tableData.map((entity, index) => (
                            <tr key={keyExtractor ? keyExtractor(entity) : index}>
                                {activeTableColumns.map((column) => (
                                    <td key={String(column.key)}>
                                        {
                                            column.render ?
                                                column.render(entity[column.key], entity)
                                                : <span>{String(entity[column.key] ?? "-")}</span>
                                        }
                                    </td>
                                ))}
                                {tableActions && tableActions.length > 0 && (
                                    <td key="actions">
                                        {tableActions.map((action) =>
                                            !action.disabled ? (
                                                <button
                                                    key={action.key}
                                                    onClick={() => action.action(entity)}>
                                                    {action.label}
                                                </button>
                                            ) : null
                                        )}
                                    </td>
                                )}
                            </tr>
                        ))
                    }
                </tbody>
            </table>
        </div>

    )


}

