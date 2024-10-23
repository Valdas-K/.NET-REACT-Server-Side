import useSWR from 'swr';
import { getApi } from "@/api";
import { IDashboard } from "@/interfaces/IDashboard";
import { useEffect, useState } from 'react';
import { IUser } from '../../../interfaces/IUser';

export default function Dashboard() {
    const { data, error, isLoading } = useSWR<IDashboard | undefined>(
        "admin/dashboard",
        getApi, {
        revalidateOnReconnect: true
    }
    );

    const [users, setUsers] = useState<IUser[]>([])
    const getUsers = () => getApi<IUser[]>('admin/dashboard').then(s => s && setUsers(s))

    useEffect(() => {
        getUsers().then(i => 1)
    }, [])

    return <div>
        <h1 className="text-xl text-blue-950"> Admin Dashboard</h1>
        {error ? <div>{error} </div> : null}
        {data?.text}

        <ul>
            {users.map(user => (
                <li>{user.userName} {user.email}</li>
            ))}
        </ul>
    </div>
}