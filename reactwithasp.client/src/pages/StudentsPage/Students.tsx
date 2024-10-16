import { useEffect, useState } from "react";
import { IStudent } from "@/interfaces/IStudent";
import { getApi, postApi, putApi } from "@/api";
import { Modal } from "../components/Modal";
import { StudentForm } from "./components/StudentForm";
import { WrenchScrewdriverIcon } from "@heroicons/react/24/outline";

export default function Students() {
    const [students, setStudents] = useState<IStudent[]>([])
    const [visibleModal, setVisibleModal] = useState<boolean>(false)
    const [editStudent, setEditStudent] = useState<IStudent | undefined>()

    const getStudents = () => getApi<IStudent[]>('students').then(s => s && setStudents(s))

    const storeStudent = (student: IStudent) => {
        if (student.id) {
            putApi(`students/${student.id}`, student).then(r => getStudents()).then(i => 1)
        } else {
            postApi(`students/${student.id}`, student).then(r => getStudents()).then(i => 1)
        }
        setVisibleModal(false)
    }

    const editHandler = (student?: IStudent | undefined) => {
        setVisibleModal(true)
        setEditStudent(student)
    }

    useEffect(() => {
        getStudents().then(i => 1)
    }, [])


    return <div>
        {
            visibleModal ? <Modal visibleModal={visibleModal} setVisibleModal={setVisibleModal} title='Studentų forma'>
                <StudentForm storeStudent={storeStudent} student={editStudent} />
            </Modal> : null
        }
        <div className="text-3xl"> Students </div>
        <div className="text-1xl">
            <button type="button" onClick={() => editHandler()}> New Student </button>
        </div>
        <div>
            {
                students.map(student => <div key={student.id}>
                    <button type="button" onClick={() => editHandler(student)}>
                        <WrenchScrewdriverIcon className="h-5 w-5 text-blue-500" /> </button>
                    {student.firstName} {student.lastName} {student.email}</div>) 
            }
        </div>
    </div>
}