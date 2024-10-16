import { useEffect, useState } from "react";
import { ILecturer } from "@/interfaces/ILecturer";
import { getApi, postApi, putApi, deleteApi } from "@/api";
import { Modal } from "../components/Modal";
import { LecturerForm } from "./components/LecturerForm";
import { WrenchScrewdriverIcon, TrashIcon } from "@heroicons/react/24/outline";

export default function Lecturers() {
    const [lecturers, setLecturers] = useState<ILecturer[]>([])
    const [visibleModal, setVisibleModal] = useState<boolean>(false)
    const [editLecturer, setEditLecturer] = useState<ILecturer | undefined>()

    const getLecturers = () => getApi<ILecturer[]>('lecturers').then(s => s && setLecturers(s))

    const storeLecturer = (lecturer: ILecturer) => {
        if (lecturer.id) {
            putApi(`lecturers/${lecturer.id}`, lecturer).then(r => getLecturers()).then(i => 1)
        } else {
            postApi(`lecturers/${lecturer.id}`, lecturer).then(r => getLecturers()).then(i => 1)
        }
        setVisibleModal(false)
    }

    const editHandler = (lecturer?: ILecturer | undefined) => {
        setVisibleModal(true)
        setEditLecturer(lecturer)
    }

    const deleteHandler = (lecturer: ILecturer) => {
        deleteApi(`lecturers/${lecturer.id}`, lecturer).then(r => getLecturers()).then(i => 1)
    }

    useEffect(() => {
        getLecturers().then(i => 1)
    }, [])


    return <div>
        {
            visibleModal ? <Modal visibleModal={visibleModal} setVisibleModal={setVisibleModal} title='Dėstytojų forma'>
                <LecturerForm storeLecturer={storeLecturer} lecturer={editLecturer} />
            </Modal> : null
        }
        <div className="text-3xl"> Lecturers </div>
        <div className="text-1xl">
            <button type="button" onClick={() => editHandler()}> New Lecturer </button>
        </div>
        <div>
            {
                lecturers.map(lecturer => <div key={lecturer.id}>
                    <button type="button" onClick={() => editHandler(lecturer)}>
                        <WrenchScrewdriverIcon className="h-5 w-5 text-blue-500" /> </button>
                    <button type="button" onClick={() => deleteHandler(lecturer)}>
                        <TrashIcon className="h-5 w-5 text-blue-500" /> </button>
                    {lecturer.firstName} {lecturer.lastName} {lecturer.email} {lecturer.qualification}</div>)
            }
        </div>
    </div>
}