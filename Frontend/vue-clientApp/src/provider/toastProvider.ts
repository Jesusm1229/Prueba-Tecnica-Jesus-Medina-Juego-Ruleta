import { useToast } from '@/components/ui/toast';
import { h, type RendererElement, type RendererNode, type VNode, type VNodeArrayChildren } from 'vue';
const { toast } = useToast()


export const toastMessage = (title: any, description: string | number | boolean | VNodeArrayChildren | { [name: string]: unknown; $stable?: boolean; } | VNode<RendererNode, RendererElement, { [key: string]: any; }> | (() => any) | undefined, duration: any, variant?: any) => {
    toast({
        title,
        description: h('div', { class: ' text-wrap' }, description),
        duration,
        variant
    });
}