import { useForm } from "react-hook-form";
import { useEffect } from "react";
import { IGroup } from "@/interfaces/IGroup";
import { formStyle } from "@/styles/formStyle";

type GroupFormProps = { group: IGroup | undefined; storeGroup: (data: IGroup) => void }
export function GroupForm(props: GroupFormProps) {
    const { group, storeGroup } = props
    const { register, handleSubmit, reset } = useForm<IGroup>({ defaultValues: group })

    useEffect(() => {
        reset(group);
    }, [group, reset])

    return (
        <form onSubmit={handleSubmit(storeGroup)} className='flex flex-col gap-3'>
            <input type="hidden" {...register("id")} />
            <div>
                <label htmlFor="title" className={formStyle.label}>Grupės pavadinimas</label>
                <input id="title" className={formStyle.input} {...register("title", { required: true, maxLength: 10 })} defaultValue={group?.title || ''} />
            </div>
            <button className={formStyle.button} type="submit">Išsaugoti</button>
        </form>
    )
}