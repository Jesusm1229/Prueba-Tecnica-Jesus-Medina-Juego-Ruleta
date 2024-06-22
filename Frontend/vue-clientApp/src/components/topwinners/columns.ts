import type { PlayerWithScore } from '@/lib/types';
import { h } from 'vue';
import type { ColumnDef } from '@tanstack/vue-table'

export const columns: ColumnDef<PlayerWithScore>[] = [
    {
        accessorKey: 'username',
        header: () => h('div', { class: 'text-right' }, 'Username'),
        cell: ({ row }) => h('div', { class: 'text-right' }, row.getValue('username'))
    }
];