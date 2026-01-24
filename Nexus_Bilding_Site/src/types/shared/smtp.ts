export interface Smtp {
    host: string;
    port: number;
    user: string;
    pass: string;
    secure: boolean;
    from: string;
    to: string;
    subject: string;
    text: string;
    html: string;
    attachments?: string[];
    cc?: string[];
    bcc?: string[];
    replyTo?: string;
    headers?: Record<string, string>;
    priority?: 'low' | 'normal' | 'high';
    timeout?: number;
    retries?: number;
    retryDelay?: number;
    maxRetries?: number;
}