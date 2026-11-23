import { Route, Routes } from "react-router-dom"
import { Layout } from "../UI/Components/Global/Layout"
import { TagPage } from "../Pages/Tags/tag.page"

export const Router = () => {
    return (
        <Routes>
            <Route element={<Layout />}>
                <Route path="/" element={<p>asd</p>} />
                <Route path="/tags" element={<TagPage />} />
                <Route path="/notfound" element={<p>Not Found</p>} />
                <Route path="*" element={<p>Not Found</p>} />
            </Route>
        </Routes>
    )

}