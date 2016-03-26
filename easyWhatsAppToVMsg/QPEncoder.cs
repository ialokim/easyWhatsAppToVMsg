using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyWhatsAppToVMsg
{
    class QPEncoder
    {
        private StringBuilder writer;
        private int count = 0;  // number of bytes that have been output
        private int bytesPerLine; // number of bytes per line
        private bool gotSpace = false;
        private bool gotCR = false;

        /**
         * Create a QP encoder that encodes the specified input stream
         * @param writer        the output stream
         * @param bytesPerLine  the number of bytes per line. The encoder
         *                   inserts a CRLF sequence after this many number
         *                   of bytes.
         */
        public QPEncoder(StringBuilder writer, int bytesPerLine) {
            this.writer = writer;
            // Subtract 1 to account for the '=' in the soft-return 
            // at the end of a line
            this.bytesPerLine = bytesPerLine - 1;
        }

        /**
         * Create a QP encoder that encodes the specified input stream.
         * Inserts the CRLF sequence after outputting 76 bytes.
         * @param writer        the output stream
         */
        public QPEncoder(StringBuilder writer) : this(writer, 76) { }

        /**
         * Encodes <code>len</code> bytes from the specified
         * <code>byte</code> array starting at offset <code>off</code> to
         * this output stream.
         *
         * @param      b     the data.
         * @param      off   the start offset in the data.
         * @param      len   the number of bytes to write.
         * @exception  IOException  if an I/O error occurs.
         */
        public void write(byte[] b, int off, int len) {
            for (int i = 0; i < len; i++)
                write(b[off + i]);
        }

        /**
         * Encodes <code>b.length</code> bytes to this output stream.
         * @param      b   the data to be written.
         * @exception  IOException  if an I/O error occurs.
         */
        public void write(byte[] b) {
            write(b, 0, b.Length);
        }

        /**
         * Encodes the specified <code>byte</code> to this output stream.
         * @param      c   the <code>byte</code>.
         * @exception  IOException  if an I/O error occurs.
         */
        public void write(byte c) {
            //c = c & 0xff; // Turn off the MSB.
            if (gotSpace) { // previous character was <SPACE>
                if (c == (byte)'\r' || c == '\n')
                    // if CR/LF, we need to encode the <SPACE> char
                    output((byte)' ', true);
                else // no encoding required, just output the char
                    output((byte)' ', false);
                gotSpace = false;
            }

            if (c == '\r') {
                gotCR = true;
                outputCRLF();
            } else {
                if (c == '\n') {
                    if (gotCR) 
                        // This is a CRLF sequence, we already output the 
                        // corresponding CRLF when we got the CR, so ignore this
                        ;
                    else
                        outputCRLF();
                } else if (c == ' ') {
                    gotSpace = true;
                } else if (c < 32 || c >= 127 || c == '=')
                    // Encoding required. 
                    output(c, true);
                else // No encoding required
                    output(c, false);
                // whatever it was, it wasn't a CR
                gotCR = false;
            }
        }

        private void outputCRLF() {
            writer.Append('\r');
            writer.Append('\n');
            count = 0;
        }

        protected void output(byte c, bool encode) {
            if (encode) {
                if ((count += 3) > bytesPerLine) {
                    writer.Append("=\r\n");
                    count = 3; // set the next line's length
                }
                writer.Append('=');
                writer.Append(((byte)c).ToString("X2"));
            } else {
                if (++count > bytesPerLine) {
                    writer.Append("=\r\n");
                    count = 1; // set the next line's length
                }
                writer.Append(Convert.ToChar(c));
            }
        }
    }
}
