import { Route, Routes } from "react-router-dom"
import { Layout } from "../../UI/Components/Global/Layout"

export const Router = () => {
    return (
        <Routes>
            <Route element={<Layout />}>
                <Route path="/" element={<p>asd</p>} />
                <Route path="/notfound" element={<p>Not Found</p>} />
                <Route path="*" element={<p>Not Found</p>} />
            </Route>
        </Routes>
    )

}