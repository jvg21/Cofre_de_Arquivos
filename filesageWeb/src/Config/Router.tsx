import { Route, Routes } from "react-router-dom"
import { Layout } from "../UI/Components/Global/Layout"
import { TagPage } from "../Pages/Tags/tag.page"
import { AuthorPage } from "../Pages/Author/author.page"
import { BookPage } from "../Pages/Book/book.page"

export const Router = () => {
    return (
        <Routes>
            <Route element={<Layout />}>
                <Route path="/" element={<p>asd</p>} />
                <Route path="/tags" element={<TagPage />} />
                <Route path="/authors" element={<AuthorPage />} />
                <Route path="/books" element={<BookPage />} />
                <Route path="/notfound" element={<p>Not Found</p>} />
                <Route path="*" element={<p>Not Found</p>} />
            </Route>
        </Routes>
    )

}