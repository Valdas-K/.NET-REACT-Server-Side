import { useForm } from "react-hook-form";
import { useEffect } from "react";
import { ISubject } from "@/interfaces/ISubject";
import { formStyle } from "@/styles/formStyle";

type SubjectFormProps = { subject: ISubject | undefined; storeSubject: (data: ISubject) => void }
export function SubjectForm(props: SubjectFormProps) {
    const { subject, storeSubject } = props
    const { register, handleSubmit, reset } = useForm<ISubject>({ defaultValues: subject })

    useEffect(() => {
        reset(subject);
    }, [subject, reset])

    return (
        <form onSubmit={handleSubmit(storeSubject)} className='flex flex-col gap-3'>
            <input type="hidden" {...register("id")} />
            <div>
                <label htmlFor="title" className={formStyle.label}>Paskaitos pavadinimas</label>
                <input id="title" className={formStyle.input} {...register("title", { required: true, maxLength: 30 })} defaultValue={subject?.title || ''} />
            </div>
            <div>
                <label htmlFor="description" className={formStyle.label}>Papildoma informacija</label>
                <input id="description" className={formStyle.input} {...register("description", { required: true, maxLength: 100 })} defaultValue={subject?.description || ''} />
            </div>
            <button className={formStyle.button} type="submit">Išsaugoti</button>
        </form>
    )
}