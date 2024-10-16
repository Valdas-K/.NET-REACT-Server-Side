import { useEffect, useState } from "react";
import { IGroup } from "@/interfaces/IGroup";
import { getApi, postApi, putApi, deleteApi } from "@/api";
import { Modal } from "../components/Modal";
import { GroupForm } from "./components/GroupForm";
import { WrenchScrewdriverIcon, TrashIcon } from "@heroicons/react/24/outline";

export default function Groups() {
    const [groups, setGroups] = useState<IGroup[]>([])
    const [visibleModal, setVisibleModal] = useState<boolean>(false)
    const [editGroup, setEditGroup] = useState<IGroup | undefined>()

    const getGroups = () => getApi<IGroup[]>('groups').then(s => s && setGroups(s))

    const storeGroup = (group: IGroup) => {
        if (group.id) {
            putApi(`groups/${group.id}`, group).then(r => getGroups()).then(i => 1)
        } else {
            postApi(`groups/${group.id}`, group).then(r => getGroups()).then(i => 1)
        }
        setVisibleModal(false)
    }

    const editHandler = (group?: IGroup | undefined) => {
        setVisibleModal(true)
        setEditGroup(group)
    }

    const deleteHandler = (group: IGroup) => {
        deleteApi(`groups/${group.id}`, group).then(r => getGroups()).then(i => 1)
    }

    useEffect(() => {
        getGroups().then(i => 1)
    }, [])

    return <div>
        {
            visibleModal ? <Modal visibleModal={visibleModal} setVisibleModal={setVisibleModal} title='Studijų grupių forma'>
                <GroupForm storeGroup={storeGroup} group={editGroup} />
            </Modal> : null
        }
        <div className="text-3xl"> Groups </div>
        <div className="text-1xl">
            <button type="button" onClick={() => editHandler()}> New Group </button>
        </div>
        <div>
            {
                groups.map(group => <div key={group.id}>
                    <button type="button" onClick={() => editHandler(group)}>
                        <WrenchScrewdriverIcon className="h-5 w-5 text-blue-500" /> </button>
                    <button type="button" onClick={() => deleteHandler(group)}>
                        <TrashIcon className="h-5 w-5 text-blue-500" /> </button>
                    {group.title}</div>) 
            }
        </div>
    </div>
}