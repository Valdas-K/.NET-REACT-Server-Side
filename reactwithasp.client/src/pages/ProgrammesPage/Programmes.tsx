import { useEffect, useState } from "react";
import { IProgramme } from "@/interfaces/IProgramme";
import { getApi, postApi, putApi, deleteApi } from "@/api";
import { Modal } from "../components/Modal";
import { ProgrammeForm } from "./components/ProgrammeForm";
import { WrenchScrewdriverIcon, TrashIcon } from "@heroicons/react/24/outline";

export default function Programmes() {
    const [programmes, setProgrammes] = useState<IProgramme[]>([])
    const [visibleModal, setVisibleModal] = useState<boolean>(false)
    const [editProgramme, setEditProgramme] = useState<IProgramme | undefined>()

    const getProgrammes = () => getApi<IProgramme[]>('programmes').then(s => s && setProgrammes(s))

    const storeProgramme = (programme: IProgramme) => {
        if (programme.id) {
            putApi(`programmes/${programme.id}`, programme).then(r => getProgrammes()).then(i => 1)
        } else {
            postApi(`programmes/${programme.id}`, programme).then(r => getProgrammes()).then(i => 1)
        }
        setVisibleModal(false)
    }

    const editHandler = (programme?: IProgramme | undefined) => {
        setVisibleModal(true)
        setEditProgramme(programme)
    }

    const deleteHandler = (programme: IProgramme) => {
        deleteApi(`programmes/${programme.id}`, programme).then(r => getProgrammes()).then(i => 1)
    }

    useEffect(() => {
        getProgrammes().then(i => 1)
    }, [])

    return <div>
        {
            visibleModal ? <Modal visibleModal={visibleModal} setVisibleModal={setVisibleModal} title='Programų forma'>
                <ProgrammeForm storeProgramme={storeProgramme} programme={editProgramme} />
            </Modal> : null
        }
        <div className="text-3xl"> Programmes </div>
        <div className="text-1xl">
            <button type="button" onClick={() => editHandler()}> New programme </button>
        </div>
        <div>
            {
                programmes.map(programme => <div key={programme.id}>
                    <button type="button" onClick={() => editHandler(programme)}>
                        <WrenchScrewdriverIcon className="h-5 w-5 text-blue-500" /> </button>
                    <button type="button" onClick={() => deleteHandler(programme)}>
                        <TrashIcon className="h-5 w-5 text-blue-500" /> </button>
                    {programme.title} {programme.description}</div>) 
            }
        </div>
    </div>
}