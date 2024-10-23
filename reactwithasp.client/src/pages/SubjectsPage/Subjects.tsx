import { useEffect, useState } from "react";
import { ISubject } from "@/interfaces/ISubject";
import { getApi, postApi, putApi, deleteApi } from "@/api";
import { Modal } from "../components/Modal";
import { SubjectForm } from "./components/SubjectForm";
import { WrenchScrewdriverIcon, TrashIcon } from "@heroicons/react/24/outline";

export default function Subjects() {
    const [subjects, setSubjects] = useState<ISubject[]>([])
    const [visibleModal, setVisibleModal] = useState<boolean>(false)
    const [editSubject, setEditSubject] = useState<ISubject | undefined>()

    const getSubjects = () => getApi<ISubject[]>('subjects').then(s => s && setSubjects(s))

    const storeSubject = (subject: ISubject) => {
        if (subject.id) {
            putApi(`subjects/${subject.id}`, subject).then(r => getSubjects()).then(i => 1)
        } else {
            subject.id = 1
            postApi(`subjects/${subject.id}`, subject).then(r => getSubjects()).then(i => 1)
        }
        setVisibleModal(false)
    }

    const editHandler = (subject?: ISubject) => {
        setVisibleModal(true)
        setEditSubject(subject)
    }

    const deleteHandler = (subject: ISubject) => {
        deleteApi(`subjects/${subject.id}`, subject).then(r => getSubjects()).then(i => 1)
    }

    useEffect(() => {
        getSubjects().then(i => 1)
    }, [])


    return <div>
        {
            visibleModal ? <Modal visibleModal={visibleModal} setVisibleModal={setVisibleModal} title='Paskaitų forma'>
                <SubjectForm storeSubject={storeSubject} subject={editSubject} />
            </Modal> : null
        }
        <div className="text-3xl"> Subjects </div>
        <div className="text-1xl">
            <button type="button" onClick={() => editHandler()}> New Subject </button>
        </div>
        <div>
            {
                subjects.map(subject => <div key={subject.id}>
                    <button type="button" onClick={() => editHandler(subject)}>
                        <WrenchScrewdriverIcon className="h-5 w-5 text-blue-500" /> </button>
                    <button type="button" onClick={() => deleteHandler(subject)}>
                        <TrashIcon className="h-5 w-5 text-blue-500" /> </button>
                    {subject.title} {subject.description}</div>) 
            }
        </div>
    </div>
}