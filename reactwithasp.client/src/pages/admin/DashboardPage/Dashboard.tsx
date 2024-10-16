import useSWR from 'swr';
import { getApi } from "@/api";
import { IDashboard } from "@/interfaces/IDashboard";

export default function Dashboard() {
    const { data, error, isLoading } = useSWR<IDashboard | undefined>(
        "admin/dashboard",
        getApi, {
        revalidateOnReconnect: true
    }
    );

    return <div>
        <h1 className="text-xl text-blue-950"> Admin Dashboard</h1>
        {error ? <div>{error} </div> : null}
        {data?.text}
    </div>
}