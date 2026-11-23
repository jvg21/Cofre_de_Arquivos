import { BrowserRouter } from "react-router-dom"
import { Router } from "./Config/Router"
import { NotificationProvider } from "./Data/Context/notification.context"
import { ModalProvider } from "./Data/Context/modal.context"

function App() {

  return (
    <>
      <BrowserRouter>
        <NotificationProvider>
          <ModalProvider>
            <Router />
          </ModalProvider>
        </NotificationProvider>
      </BrowserRouter>
    </>
  )
}

export default App
