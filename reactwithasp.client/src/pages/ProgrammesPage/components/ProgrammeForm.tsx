import { useForm } from "react-hook-form";
import { useEffect } from "react";
import { IProgramme } from "@/interfaces/IProgramme";
import { formStyle } from "@/styles/formStyle";

type ProgrammeFormProps = { programme: IProgramme | undefined; storeProgramme: (data: IProgramme) => void }
export function ProgrammeForm(props: ProgrammeFormProps) {
    const { programme, storeProgramme } = props
    const { register, handleSubmit, reset } = useForm<IProgramme>({ defaultValues: programme })

    useEffect(() => {
        reset(programme);
    }, [programme, reset])

    return (
        <form onSubmit={handleSubmit(storeProgramme)} className='flex flex-col gap-3'>
            <input type="hidden" {...register("id")} />
            <div>
                <label htmlFor="title" className={formStyle.label}>Pavadinimas</label>
                <input id="title" className={formStyle.input} {...register("title", { required: true, maxLength: 30 })} defaultValue={programme?.title || ''} />
            </div>
            <div>
                <label htmlFor="description" className={formStyle.label}>Papildoma informacija</label>
                <input id="description" className={formStyle.input} {...register("description", { required: true, maxLength: 100 })} defaultValue={programme?.description || ''} />
            </div>
            <button className={formStyle.button} type="submit">Išsaugoti</button>
        </form>
    )
}