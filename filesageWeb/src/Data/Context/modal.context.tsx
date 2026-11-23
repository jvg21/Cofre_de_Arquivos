import { createContext, useContext, useState, type ReactNode } from "react";
import style from './modal.module.css'

type ModalSize = 'sm' | 'md' | 'lg';

interface ModalProps {
    isOpen: boolean;
    setModal: (content: ReactNode, styleProps?: ModalSize, isOpen?: boolean) => boolean;
    closeModal: () => void;
}

const ModalContext = createContext<ModalProps | undefined>(undefined);

export const ModalProvider = ({ children }: { children: ReactNode }) => {
    const [isOpen, setOpen] = useState(false);
    const [modalSize, setModalSize] = useState<ModalSize>("sm");
    const [modalContent, setModalContent] = useState<ReactNode | null>(null);

    function setModal(content?: ReactNode, modalSize = "sm" as ModalSize, isOpen = true) {
        setModalSize(modalSize)
        setModalContent(content ?? null);
        setOpen(isOpen);
        return true;
    }

    function closeModal() {
        setOpen(false);
        setModalSize("sm");
        setModalContent(null);
    }

    return (
        <ModalContext.Provider value={{ isOpen, setModal, closeModal }}>
            {children}

            {isOpen && (
                <ModalFrame closeModal={closeModal} modalSize={modalSize}>
                    {modalContent}
                </ModalFrame>
            )}
        </ModalContext.Provider>
    );
};

// eslint-disable-next-line react-refresh/only-export-components
export const useModal = () => {
    const context = useContext(ModalContext);

    if (!context) {
        throw new Error(
            "useModal must be used within a ModalProvider"
        );
    }

    return context;
};

interface ModalFrame {
    children: ReactNode,
    modalSize?: ModalSize
    closeModal: (modal: boolean) => void
}


export const ModalFrame = ({ children, closeModal, modalSize }: ModalFrame) => {
    const [clickTimeout, setClickTimeout] = useState<ReturnType<typeof setTimeout> | null>(null)

    const handleCloseClick = () => {

        if (clickTimeout !== null) {
            clearTimeout(clickTimeout);
            setClickTimeout(null);
            closeModal(false)
        }

        setClickTimeout(setTimeout(() => {
            setClickTimeout(null)
        }, 1000))

    }
    return (
        <div className={`${style.modalBackground}`} onClick={handleCloseClick}>
            <div className={`${style.modal} ${modalSize ? style[modalSize] : ''}`} onClick={(e) => e.stopPropagation()}>
                <header className={style.modalHeader}>

                    <button onClick={() => closeModal(false)}> X </button>
                </header>
                <div className={style.modalBody}>
                    {children}
                </div>
            </div>

        </div>
    )
}
