import { useForm } from "react-hook-form";
import { useEffect } from "react";
import { ILecturer } from "@/interfaces/ILecturer";
import { formStyle } from "@/styles/formStyle";

type LecturerFormProps = { lecturer: ILecturer | undefined; storeLecturer: (data: ILecturer) => void }
export function LecturerForm(props: LecturerFormProps) {
    const { lecturer, storeLecturer } = props
    const { register, handleSubmit, reset } = useForm<ILecturer>({ defaultValues: lecturer })

    useEffect(() => {
        reset(lecturer);
    }, [lecturer, reset])

    return (
        <form onSubmit={handleSubmit(storeLecturer)} className='flex flex-col gap-3'>
            <input type="hidden" {...register("id")} />
            <div>
                <label htmlFor="firstName" className={formStyle.label}>Vardas</label>
                <input id="firstName" className={formStyle.input} {...register("firstName", { required: true, maxLength: 30 })} defaultValue={lecturer?.firstName || ''} />
            </div>
            <div>
                <label htmlFor="lastName" className={formStyle.label}>Pavardė</label>
                <input id="lastName" className={formStyle.input} {...register("lastName", { required: true, maxLength: 30 })} defaultValue={lecturer?.lastName || ''} />
            </div>
            <div>
                <label htmlFor="email" className={formStyle.label}>El. paštas</label>
                <input id="email" className={formStyle.input} type="email" {...register("email", { required: true, maxLength: 40 })} defaultValue={lecturer?.email || ''} />
            </div>
            <div>
                <label htmlFor="qualification" className={formStyle.label}>Dėstytojo kvalifikacijos</label>
                <input id="qualification" className={formStyle.input} {...register("qualification", { required: true, maxLength: 100 })} defaultValue={lecturer?.qualification || ''} />
            </div>
            <button className={formStyle.button} type="submit">Išsaugoti</button>
        </form>
    )
}