<script setup>
// Adjust the import path as necessary
import { Table, TableHeader, TableRow, TableHead, TableBody, TableCell } from '@/components/ui/table' // Adjust the import path as necessary
import {
    FlexRender,
    getCoreRowModel,
    useVueTable,
} from '@tanstack/vue-table'


defineProps({
    data: Array,
    columns: Array,
});

const table = useVueTable({
    get data() { return props.data },
    get columns() { return props.columns },
    getCoreRowModel: getCoreRowModel(),
});
</script>

<template>
    <div class="border rounded-md">
        <Table>
            <TableHeader>
                <TableRow v-for="headerGroup in table.getHeaderGroups()" :key="headerGroup.id">
                    <TableHead v-for="header in headerGroup.headers" :key="header.id">
                        <FlexRender v-if="!header.isPlaceholder" :render="header.column.columnDef.header"
                            :props="header.getContext()" />
                    </TableHead>
                </TableRow>
            </TableHeader>
            <TableBody>
                <template v-if="table.getRowModel().rows?.length">
                    <TableRow v-for="row in table.getRowModel().rows" :key="row.id"
                        :data-state="row.getIsSelected() ? 'selected' : undefined">
                        <TableCell v-for="cell in row.getVisibleCells()" :key="cell.id">
                            <FlexRender :render="cell.column.columnDef.cell" :props="cell.getContext()" />
                        </TableCell>
                    </TableRow>
                </template>
                <template v-else>
                    <TableRow>
                        <TableCell :colspan="props.columns.length" class="h-24 text-center">
                            No results.
                        </TableCell>
                    </TableRow>
                </template>
            </TableBody>
        </Table>
    </div>
</template>